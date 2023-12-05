<template>
    <div v-if="this.$store.state.stops" class="content">
        <h2>Stops:</h2>
        <table class="styled-table">
            <thead>
                <tr>
                    <th>Stop ID</th>
                    <th>Stop Name</th>
                    <th>Latitude</th>
                    <th>Longitude</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="stop in this.$store.state.stops" :key="stop.stopId">
                    <td>{{ stop.stopId }}</td>
                    <td>{{ stop.stopDesc }}</td>
                    <td>{{ stop.stopLat }}</td>
                    <td>{{ stop.stopLon }}</td>
                    <td>
                        <button v-on:click="showStopDetails(stop.stopId)">Show delays</button>
                        <button v-if="this.$store.state.loggedIn" v-on:click="addToUserFav(stop.stopId)">Add to favourites</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    export default {
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