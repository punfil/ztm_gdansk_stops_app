<script setup>

</script>

<template>
    <ul class="horizontal-menu">
        <li v-on:click="fetchUsers">List users</li>
        <li v-on:click="fetchStops">List stops</li>
        <li v-if="!loggedIn" v-on:click="loginTrigger">Login</li>
        <li v-if="loggedIn" v-on:click="listUserStops">List user favourite stops</li>
    </ul>
    <h1>
        RAILAB4 184657 Panfil Wojciech - zkm_gdansk_stops_app
    </h1>
    <header v-if="msg" class="error">
        {{ msg }}
    </header>
    <!---Placebo for loading -->
    <div v-if="loading" class="loading">
        Fetching latest data just for you!
    </div>
    <!-- List users -->
    <div v-if="usersList" class="content">
        <button v-on:click="addUserTrigger">Add user</button>
        <h2>Users:</h2>
        <table class="styled-table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Login</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in usersList" :key="user.Id">
                    <td>{{ user.Id }}</td>
                    <td>{{ user.Login }}</td>
                    <td v-if="user.Id != this.loggedIn" v-on:click="deleteUser(user.Id)">Remove user</td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- List stops -->
    <div v-if="stops" class="content">
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
                <tr v-for="stop in stops" :key="stop.stopId">
                    <td>{{ stop.stopId }}</td>
                    <td>{{ stop.stopDesc }}</td>
                    <td>{{ stop.stopLat }}</td>
                    <td>{{ stop.stopLon }}</td>
                    <td>
                        <button v-on:click="showStopDetails(stop.stopId)">Show delays</button>
                        <button v-if="loggedIn" v-on:click="addToUserFav(stop.stopId)">Add to favourites</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!--- Stop info -->
    <div v-if="stopInfo" class="content">
        <h2>Stop details:</h2>
        <p>Last update: {{ stopInfo.lastUpdate }}</p>
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
                <tr v-for="delayInfo in stopInfo.delay" :key="delayInfo.id">
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
    <!-- Add user form -->
    <div v-if="addUserMode">
        <h2>Add new user:</h2>
        <form @submit.prevent="addUser">
            <label for="id">User ID:</label>
            <input type="number" v-model="id" required>

            <label for="username">Username:</label>
            <input type="text" v-model="username" required>

            <label for="password">Password:</label>
            <input type="password" v-model="password" required>

            <button type="submit">Add User</button>
        </form>
    </div>
    <!-- Login form -->
    <div v-if="loginMode">
        <h2>Login:</h2>
        <form @submit.prevent="login">
            <label for="username">Username:</label>
            <input type="text" v-model="username" required>

            <label for="password">Password:</label>
            <input type="password" v-model="password" required>

            <button type="submit">Login</button>
        </form>
    </div>
    <!-- List user stops -->
    <div v-if="userStops" class="content">
        <h2>User stops:</h2>
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
                <tr v-for="stop in stops" :key="stop.stopId">
                    <td>{{ stop.stopId }}</td>
                    <td>{{ stop.stopDesc }}</td>
                    <td>{{ stop.stopLat }}</td>
                    <td>{{ stop.stopLon }}</td>
                    <td>
                        <button v-on:click="showStopDetails(stop.stopId)">Show delays</button>
                        <button v-if="loggedIn" v-on:click="removeFromUserFav(stop.stopId)">Remove from favourites</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                loggedIn: null,
                usersList: null,
                stops: null,
                stopInfo: null,
                addUserMode: false,
                loginMode: false,
                userStops: null,
                msg: null,
            }
        },
        created() {
            
        },
        methods: {
            setAllDisplaysNull() {
                this.usersList = null;
                this.stops = null;
                this.stopInfo = null;
                this.addUserMode = false;
                this.loginMode = false;
                this.userStops = null;
                this.msg = null;
            },
            fetchUsers() {
                this.loading = true;
                this.setAllDisplaysNull();
                fetch('listusers')
                    .then(r => r.json())
                    .then(json => {
                        this.usersList = json;
                        this.loading = false;
                        return;
                    });
            },
            fetchStops() {
                this.loading = true;
                this.setAllDisplaysNull();
                fetch('stops')
                    .then(r => r.json())
                    .then(json => {
                        this.stops = json[this.getFormattedDate()].stops;
                        this.loading = false;
                        return;
                    });
            },
            showStopDetails(stopID) {
                this.loading = true;
                this.setAllDisplaysNull();
                fetch('stopinfo/'+stopID)
                    .then(r => r.json())
                    .then(json => {
                        this.stopInfo = json;
                        this.loading = false;
                        return;
                    });
            },
            getFormattedDate() {
                const today = new Date();
                const year = today.getFullYear();
                let month = today.getMonth() + 1;
                let day = today.getDate();

                // Add leading zero for single-digit months and days
                month = month < 10 ? `0${month}` : month;
                day = day < 10 ? `0${day}` : day;

                return `${year}-${month}-${day}`;
            },
            addUserTrigger() {
                this.setAllDisplaysNull();
                this.addUserMode = true;
            },
            addUser() {
                const url = `adduser/${this.id}&${this.username}&${this.password}`;
                this.setAllDisplaysNull();
                fetch(url, {
                    method: 'POST',
                })
                    .then(response => {
                        if (!response.ok) {
                            this.msg = "Failed to add user."
                            return;
                        }

                        this.msg = "User added!";
                        return;
                    })
            },
            deleteUser(userID) {
                this.setAllDisplaysNull();
                fetch('deleteuser/' + userID, { method: "DELETE" })
                    .then(response => {
                        if (!response.ok) {
                            this.msg = "Failed to remove user."
                            return;
                        }

                        this.msg = "User removed!";
                        return;
                    })
            },
            loginTrigger() {
                this.setAllDisplaysNull();
                this.loginMode = true;
            },
            login() {
                const url = `login/${this.username}&${this.password}`;
                this.setAllDisplaysNull();
                fetch(url, {
                    method: 'POST',
                })
                    .then(response => {
                        if (!response.ok) {
                            this.msg = "Failed to login."
                            return;
                        }

                        return response.json();
                    })
                    .then(json => {
                        this.loggedIn = json;
                    })
            },
            listUserStops() {
                const url = `listuserstops/${this.loggedIn}`;
                this.setAllDisplaysNull();
                fetch(url)
                    .then(response => {
                        if (!response.ok) {
                            this.msg = "Failed to show user stops."
                            return;
                        }

                        return response.json();
                    })
                    .then(json => {
                        this.userStops = json;
                        this.loading = false;
                        return;
                    });
            }
        }
    })
</script>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

.styled-table th,
.styled-table td {
    border: 1px solid #ddd;
    padding: 8px;
    text-align: left;
}

.styled-table th {
    background-color: black;
}

.styled-table tr:hover {
    background-color: darkslategray;
}

.horizontal-menu {
    list-style-type: none;
    margin: 0;
    padding: 0;
    display: flex;
}

.horizontal-menu li {
    padding: 10px;
    color: gray;
    cursor: pointer;
    transition: color 0.3s ease;
}

.horizontal-menu li:hover {
    color: black;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
    color: red;
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
