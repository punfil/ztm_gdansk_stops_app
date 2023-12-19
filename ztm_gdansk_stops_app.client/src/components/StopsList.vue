<template>
    <div v-if="this.$store.state.stops" class="content">
        <h2>Stops:</h2>
        <vue-good-table :columns="columns" :rows="this.$store.state.stops">
            <template #table-row="props">
                <span v-if="props.column.field == 'action'">
                    <button data-cy="showStopDetails" v-on:click="showStopDetails(props.row.stopId)">Show delays</button>
                    <button v-if="this.$store.state.loggedIn" v-on:click="addToUserFav(props.row.stopId)">Add to favourites</button>
                </span>
            </template>
        </vue-good-table>
    </div>
</template>

<script>
    import { ref } from 'vue';
    import { VueGoodTable } from 'vue-good-table-next';
    import 'vue-good-table-next/dist/vue-good-table-next.css'
    export default {
        components: {
            VueGoodTable,
        },
        setup() {
            const columns = ref([
                { label: 'Stop ID', field: 'stopId' },
                { label: 'Stop Name', field: 'stopDesc' },
                { label: 'Latitude', field: 'stopLat' },
                { label: 'Longitude', field: 'stopLon' },
                { label: 'Action', field: 'action', sortable: false },
            ]);

            return {
                columns,
            };
        },
        methods: {
            addToUserFav(stopId) {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                fetch(`addstop/${this.$store.state.loggedIn.id}&${stopId}`, {
                    method: 'POST',
                })
                    .then(response => {
                        this.$store.commit('setLoading', false);
                        if (!response.ok) {
                            this.$store.commit('setMsg', "Failed to add to favourites.");
                            return;
                        }

                        return;
                    })
            },
            showStopDetails(stopID) {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                fetch('stopinfo/' + stopID)
                    .then(r => r.json())
                    .then(json => {
                        this.$store.commit('setStopInfo', json);
                        this.$store.commit('setLoading', false);
                        return;
                    });
            },
        }
    }
</script>