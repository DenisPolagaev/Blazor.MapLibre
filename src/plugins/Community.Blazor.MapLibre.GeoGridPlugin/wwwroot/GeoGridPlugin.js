import { GeoGrid } from './geogrid/index.js';

let mapObject = null;
let geoGridInstance = null;
let stylesheetLoaded = false;

function ensureStylesheet() {
    if (stylesheetLoaded) {
        return;
    }

    const href = new URL('geogrid/geogrid.css', import.meta.url).href;
    if (!document.querySelector(`link[href="${href}"]`)) {
        const link = document.createElement('link');
        link.rel = 'stylesheet';
        link.href = href;
        document.head.appendChild(link);
    }

    stylesheetLoaded = true;
}

function resolveGridDensity(options) {
    if (options.gridDensityDegrees != null) {
        const density = options.gridDensityDegrees;
        delete options.gridDensityDegrees;
        options.gridDensity = () => density;
        return;
    }

    if (Array.isArray(options.gridDensityByZoom) && options.gridDensityByZoom.length > 0) {
        const steps = [...options.gridDensityByZoom].sort((left, right) => left.zoom - right.zoom);
        delete options.gridDensityByZoom;
        options.gridDensity = (zoomLevel) => {
            const zoom = Math.max(Math.floor(zoomLevel), 0);
            let density = steps[0].densityDegrees;
            for (const step of steps) {
                if (zoom >= step.zoom) {
                    density = step.densityDegrees;
                }
            }

            return density;
        };
    }
}

function resolveLabelFormat(options) {
    switch (options.labelFormat) {
        case 'DegreesOnly':
            options.formatLabels = (degreesFloat) => `${Math.floor(degreesFloat)}°`;
            break;
        case 'IntegerDegrees':
            options.formatLabels = (degreesFloat) => String(Math.round(degreesFloat));
            break;
        default:
            break;
    }

    delete options.labelFormat;
}

function normalizeOptions(options) {
    if (!options) {
        return {};
    }

    const normalized = { ...options };
    resolveGridDensity(normalized);
    resolveLabelFormat(normalized);
    return normalized;
}

function ensureGridVisible() {
    if (!geoGridInstance || !mapObject) {
        return;
    }

    if (typeof mapObject.loaded === 'function' && mapObject.loaded()) {
        geoGridInstance.add();
    }
}

export async function initialize(map) {
    mapObject = map;
    ensureStylesheet();
}

export function add(options) {
    if (!mapObject) {
        throw new Error('GeoGrid plugin is not initialized.');
    }

    remove();

    const normalized = normalizeOptions(options);
    geoGridInstance = new GeoGrid({
        map: mapObject,
        ...normalized,
    });

    ensureGridVisible();
}

export function remove() {
    if (geoGridInstance) {
        geoGridInstance.remove();
        geoGridInstance = null;
    }
}

export function show() {
    if (!geoGridInstance) {
        throw new Error('GeoGrid is not created. Call add() first.');
    }

    geoGridInstance.add();
}

export function dispose() {
    remove();
    mapObject = null;
}
