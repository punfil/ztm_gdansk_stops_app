import Header from '@/components/InfoHeader.vue';
export default {
    install: (app, options) => {
        app.component("info-header", Header);
    },
};