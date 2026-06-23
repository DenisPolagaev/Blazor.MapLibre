const scriptBase = '/_content/TerraDrawPlugin/terra-draw/dist/';

let mapObject;
let drawControl;
let editedFeatureId;
let dependenciesLoaded = false;

function loadScript(src) {
    if (document.querySelector(`script[src="${src}"]`)) {
        return Promise.resolve();
    }

    return new Promise((resolve, reject) => {
        const script = document.createElement('script');
        script.src = src;
        script.onload = resolve;
        script.onerror = reject;
        document.head.appendChild(script);
    });
}

async function loadDependencies() {
    if (dependenciesLoaded) {
        return;
    }

    await loadScript(`${scriptBase}terra-draw.umd.js`);
    await loadScript(`${scriptBase}terra-draw-maplibre-gl-adapter.umd.js`);
    await loadScript(`${scriptBase}TerraDrawCoordinateDeleteMode.js`);
    dependenciesLoaded = true;
}

export async function initialize(map) {
    await loadDependencies();
    mapObject = map;
}

function filterInternalFeatures(features) {
    return features.filter(f =>
        !f.properties?.coordinatePoint &&
        !f.properties?.closingPoint &&
        !f.properties?.snappingPoint &&
        !f.properties?.midPoint &&
        !f.properties?.selectionPoint
    );
}

function throttle(func, intervalInMs) {
    let lastFunc;
    let lastRan;

    return function (...args) {
        if (!lastRan) {
            func.apply(this, args);
            lastRan = Date.now();
        } else {
            clearTimeout(lastFunc);
            lastFunc = setTimeout(() => {
                if ((Date.now() - lastRan) >= intervalInMs) {
                    func.apply(this, args);
                    lastRan = Date.now();
                }
            }, intervalInMs - (Date.now() - lastRan));
        }
    };
}

export function addTerraDrawTool(options) {
    const singleFeature = options?.singleFeature === true;
    const drawModeOpts = singleFeature ? { pointerDistance: -1 } : {};
    const adapter = new terraDrawMaplibreGlAdapter.TerraDrawMapLibreGLAdapter({ map: mapObject });
    const select = new terraDraw.TerraDrawSelectMode({
        flags: {
            point: {
                feature: {
                    draggable: true
                }
            },
            polygon: {
                validation: (feature, { updateType }) => {
                    if (updateType === "finish" || updateType === "commit") {
                        return terraDraw.ValidateNotSelfIntersecting(feature);
                    }
                    return { valid: true };
                },
                feature: {
                    coordinates: {
                        midpoints: true,
                        draggable: true,
                        snappable: true
                    }
                }
            },
            linestring: {
                feature: {
                    coordinates: {
                        midpoints: true,
                        draggable: true,
                        snappable: true
                    }
                }
            }
        }
    });
    const polygonMode = new terraDraw.TerraDrawPolygonMode({
        showCoordinatePoints: true,
        ...drawModeOpts,
        validation: (feature, { updateType }) => {
            if (updateType === "finish" || updateType === "commit") {
                return terraDraw.ValidateNotSelfIntersecting(feature);
            }
            return { valid: true };
        }
    });
    const lineStringMode = new terraDraw.TerraDrawLineStringMode({
        showCoordinatePoints: true,
        ...drawModeOpts
    });
    const deleteMode = new TerraDrawCoordinateDeleteModeUmd();
    drawControl = new terraDraw.TerraDraw({
        adapter,
        modes: [
            new terraDraw.TerraDrawFreehandMode(),
            polygonMode,
            lineStringMode,
            select,
            new terraDraw.TerraDrawPointMode(),
            deleteMode
        ]
    });
}

export function stopTerraDraw() {
    if (!drawControl) {
        return;
    }

    drawControl.setMode("static");
    drawControl.stop();
    editedFeatureId = null;
}

export function setTerraDrawMode(mode) {
    if (!drawControl) {
        return;
    }

    try {
        if (!drawControl._enabled) {
            drawControl.start();
        }

        if (mode !== 'select' && editedFeatureId != null) {
            editedFeatureId = null;
        }

        if (mode === 'linestring' || mode === 'polygon') {
            drawControl.updateModeOptions(mode, { showCoordinatePoints: true });
        }

        drawControl.setMode(mode);

        for (const drawingMode of ['linestring', 'polygon']) {
            if (drawingMode !== mode) {
                drawControl.updateModeOptions(drawingMode, { showCoordinatePoints: false });
            }
        }
    } catch (e) {
        console.error('[setTerraDrawMode] Failed to set mode', mode, e);
        throw e;
    }
}

export function editTerraDrawFeature(featureJson, mode) {
    if (!drawControl) {
        console.error('[editTerraDrawFeature] terra-draw not initialised');
        return;
    }

    if (!drawControl._enabled) {
        drawControl.start();
    }

    if (editedFeatureId != null) {
        try { drawControl.removeFeatures([editedFeatureId]); } catch (e) { /* best-effort */ }
        editedFeatureId = null;
    }

    const feature = JSON.parse(featureJson);
    feature.type = "Feature";
    feature.properties = feature.properties || {};
    feature.properties.mode = mode;

    if (mode === 'linestring' || mode === 'polygon') {
        drawControl.updateModeOptions(mode, { showCoordinatePoints: true });
    }
    for (const drawingMode of ['linestring', 'polygon']) {
        if (drawingMode !== mode) {
            drawControl.updateModeOptions(drawingMode, { showCoordinatePoints: false });
        }
    }

    let result;
    try {
        result = drawControl.addFeatures([feature]);
    } catch (e) {
        console.error('[editTerraDrawFeature] addFeatures threw', e, feature);
        return;
    }

    const entry = Array.isArray(result) ? result[0] : null;
    if (entry && entry.valid === false) {
        console.error('[editTerraDrawFeature] terra-draw rejected feature:', entry.reason, feature);
        return;
    }

    drawControl.setMode('select');

    const id = entry?.id;
    if (id != null) {
        editedFeatureId = id;
        if (typeof drawControl.selectFeature === 'function') {
            try { drawControl.selectFeature(id); } catch (e) { /* selection is best-effort */ }
        }
    }
}

export function finishGeometry() {
    const event = new KeyboardEvent("keyup", { key: "Enter" });
    mapObject.getCanvas().dispatchEvent(event);
}

export function getTerraDrawGeometries() {
    if (!drawControl) {
        return [];
    }

    const all = drawControl.getSnapshot();
    if (editedFeatureId != null) {
        return all.filter(f => f.id === editedFeatureId);
    }

    return filterInternalFeatures(all);
}

export function onTerraDrawFinish(dotnetReference) {
    if (!drawControl) {
        return;
    }

    drawControl.on("finish", (id, context) => {
        if (context.action === "draw" || context.action === "dragCoordinate") {
            const features = filterInternalFeatures(drawControl.getSnapshot());
            dotnetReference.invokeMethodAsync('Invoke', JSON.stringify(features));
        }
    });
}

export function onTerraDrawDelete(dotnetReference) {
    if (!drawControl) {
        return;
    }

    drawControl.on("change", (ids, type) => {
        if (type === "delete") {
            const features = filterInternalFeatures(drawControl.getSnapshot());
            dotnetReference.invokeMethodAsync('Invoke', JSON.stringify(features));
        }
    });
}

export function onTerraDrawChange(dotnetReference, throttleTime = 100) {
    if (!drawControl) {
        return;
    }

    const throttledInvoke = throttle(() => {
        const features = filterInternalFeatures(drawControl.getSnapshot());
        dotnetReference.invokeMethodAsync('Invoke', JSON.stringify(features));
    }, throttleTime);

    drawControl.on('change', (ids, type) => {
        if (type === 'create' || type === 'update' || type === 'delete') {
            throttledInvoke();
        }
    });
}

export function disableMapZoomGestures() {
    if (!mapObject) {
        return;
    }

    mapObject.doubleClickZoom.disable();
    mapObject.touchZoomRotate?._tapDragZoom?.disable?.();
}

export function enableMapZoomGestures() {
    if (!mapObject) {
        return;
    }

    mapObject.doubleClickZoom.enable();
    mapObject.touchZoomRotate?._tapDragZoom?.enable?.();
}

export function dispose() {
    stopTerraDraw();
    drawControl = null;
    editedFeatureId = null;
    mapObject = null;
}
