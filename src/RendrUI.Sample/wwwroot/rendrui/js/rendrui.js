window.getBoundingClientRect = (element) => {
    return element.getBoundingClientRect();
};

window.RendrUI = window.RendrUI || {};

Object.assign(window.RendrUI, {
    getViewportWidth: () => window.innerWidth,

    registerViewportListener: (dotNetHelper, listenerId) => {
        let timeout;
        const handler = () => {
            clearTimeout(timeout);
            timeout = setTimeout(() => {
                dotNetHelper.invokeMethodAsync('OnViewportChanged', window.innerWidth);
            }, 100);
        };

        window.addEventListener('resize', handler);
        window.RendrUI.viewportListeners = window.RendrUI.viewportListeners || {};
        window.RendrUI.viewportListeners[listenerId] = handler;
        handler(); // Initial call
    },

    unregisterViewportListener: (listenerId) => {
        const handler = window.RendrUI.viewportListeners?.[listenerId];
        if (handler) {
            window.removeEventListener('resize', handler);
            delete window.RendrUI.viewportListeners[listenerId];
        }
    }
});