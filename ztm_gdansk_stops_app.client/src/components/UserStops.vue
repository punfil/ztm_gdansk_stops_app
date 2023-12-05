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
                            <dummyth ParentMessage='Id'/>
                            <dummyth ParentMessage='Delay (seconds)'/>
                            <dummyth ParentMessage='Estimated Time' />
                            <dummyth ParentMessage='Headsign' />
                            <dummyth ParentMessage='Route Id'/>
                            <dummyth ParentMessage='Trip Id'/>
                            <dummyth ParentMessage='Status' />
                            <dummyth ParentMessage='Theoretical Time'/>
                            <dummyth ParentMessage='Timestamp'/>
                            <dummyth ParentMessage='Trip'/>
                            <dummyth ParentMessage='Vehicle Code' />
                            <dummyth ParentMessage='Vehicle Id' />
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
    import Dummyth from '@/components/Dummyth.vue'
    export default {
        components: {
            Dummyth,
        },
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