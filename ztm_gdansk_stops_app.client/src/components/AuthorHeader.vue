<template>
    <h1 :class="{ 'highlight': isHighlighted }">
        RAILAB4 184657 Panfil Wojciech - zkm_gdansk_stops_app
    </h1>
    <slot></slot>
    <button @click="toggleHighlight">Trick or treat</button>
</template>

<script>
    export default {
        data() {
            return {
                isHighlighted: false,
            };
        },
        methods: {
            toggleHighlight() {
                this.isHighlighted = !this.isHighlighted;
            },
        },
    }
</script>