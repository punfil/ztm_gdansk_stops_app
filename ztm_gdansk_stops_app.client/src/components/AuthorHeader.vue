<template>
    <h1 :class="{ 'highlight': isHighlighted }">
        RAILAB4 184657 Panfil Wojciech - zkm_gdansk_stops_app
    </h1>
    <slot></slot>
    <button @click="toggleHighlight">Trick or treat</button>
</template>

<script>
    import { useToast } from "vue-toastification";
    export default {
        setup() {
            const toast = useToast();
            return { toast }
        },
        data() {
            return {
                isHighlighted: false,
            };
        },
        methods: {
            toggleHighlight() {
                this.isHighlighted = !this.isHighlighted;
                this.toast("I am the author!", {
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