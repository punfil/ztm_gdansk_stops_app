<template>
    <h1>
        Realizacja Aplikacji Internetowych - Projekt 4
    </h1>
    <slot></slot>
    <button @click="changeBackground">Switch color mode</button>
</template>

<script>
    import { useToast } from "vue-toastification";
    export default {
        setup() {
            const toast = useToast();
            return { toast }
        },
        methods: {
            changeBackground() {
                this.$store.commit('setIsLightMode', !this.$store.state.isLightMode);
                this.toast("Changed color scheme!", {
                    position: "top-right",
                    timeout: 5000,
                    closeOnClick: true,
                    pauseOnFocusLoss: true,
                    pauseOnHover: true,
                    draggable: true,
                    draggablePercent: 0.6,
                    showCloseButtonOnHover: false,
                    hideProgressBar: true,
                    closeButton: "button",
                    icon: "fas fa-rocket",
                    rtl: false
                });
            },
        },
    }
</script>