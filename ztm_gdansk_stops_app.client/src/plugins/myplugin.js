import MyHeader from '@/components/AuthorHeader.vue';
export default {
    install: (app, options) => {
        app.component("author-header", MyHeader);
    },
};