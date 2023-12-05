<template>
    <div v-if="this.$store.state.stopInfo" class="content">
        <h2>Stop details:</h2>
        <p>Last update: {{ this.$store.state.stopInfo.lastUpdate }}</p>
        <h3>Delays:</h3>
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
                <tr v-for="delayInfo in this.$store.state.stopInfo.delay" :key="delayInfo.id">
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
    </div>
</template>

<script>
    export default {
        methods: {
        }
    }
</script>