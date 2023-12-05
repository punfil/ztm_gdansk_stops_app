<template>
    <div v-if="this.$store.state.userStops" class="content">
        <h2>User stops:</h2>
        <ul>
            <li v-for="stop in this.$store.state.userStops" :key="stop.stopId">
                <strong>Stop ID:</strong> {{ stop.stopId }}<br>
                <strong>Last Update:</strong> {{ stop.lastUpdate }}<br>
                <strong>Delays:</strong>
                <table class="styled-table">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Delay (seconds)</th>
                            <th>Estimated Time</th>
                            <th>Headsign</th>
                            <th>Route Id</th>
                            <th>Trip Id</th>
                            <th>Status</th>
                            <th>Theoretical Time</th>
                            <th>Timestamp</th>
                            <th>Trip</th>
                            <th>Vehicle Code</th>
                            <th>Vehicle Id</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="delayInfo in stop.delays" :key="delayInfo.id">
                            <td>{{ delayInfo.id }}</td>
                            <td>{{ delayInfo.delayInSeconds }}</td>
                            <td>{{ delayInfo.estimatedTime }}</td>
                            <td>{{ delayInfo.headsign }}</td>
                            <td>{{ delayInfo.routeId }}</td>
                            <td>{{ delayInfo.tripId }}</td>
                            <td>{{ delayInfo.status }}</td>
                            <td>{{ delayInfo.theoreticalTime }}</td>
                            <td>{{ delayInfo.timestamp }}</td>
                            <td>{{ delayInfo.trip }}</td>
                            <td>{{ delayInfo.vehicleCode }}</td>
                            <td>{{ delayInfo.vehicleId }}</td>
                        </tr>
                    </tbody>
                </table>
                <button v-on:click="removeFromUserFav(stop.stopId)">Remove from favourite</button>
            </li>
        </ul>
    </div>
</template>

<script>
    export default {
        methods: {
            removeFromUserFav(stopId) {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                fetch(`deletestop/${this.$store.state.loggedIn.id}&${stopId}`, {
                    method: 'DELETE',
                })
                    .then(response => {
                        this.$store.commit('setLoading', false);
                        if (!response.ok) {
                            this.$store.commit('setMsg', "Failed to remove from favourites." + stopId);
                            return;
                        }

                        return response.json();
                    })
            }
        }
    }
</script>