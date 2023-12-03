<script setup>

</script>

<template>
    <ul class="horizontal-menu">
        <li v-on:click="fetchUsers">List users</li>
        <li v-on:click="fetchStops">List stops</li>
    </ul>
    <h1>
        RAILAB4 184657 Panfil Wojciech - zkm_gdansk_stops_app
    </h1>
    <header v-if="error_msg" class="error">
        {{ error_msg }}
    </header>
    <!---Placebo for loading -->
    <div v-if="loading" class="loading">
        Fetching latest data just for you!
    </div>
    <!-- List users -->
    <div v-if="usersList" class="content">
        <table class="styled-table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Login</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in usersList" :key="user.Id">
                    <td>{{ user.Id }}</td>
                    <td>{{ user.Login }}</td>
                </tr>
            </tbody>
        </table>
    </div>

    <div v-if="stops" class="content">
        <table class="styled-table">
            <thead>
                <tr>
                    <th>Stop ID</th>
                    <th>Stop Name</th>
                    <th>Latitude</th>
                    <th>Longitude</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(stop, stopId) in stops" :key="stopId">
                    <td>{{ stop.stopId }}</td>
                    <td>{{ stop.stopDesc }}</td>
                    <td>{{ stop.stopLat }}</td>
                    <td>{{ stop.stopLon }}</td>
                    <!-- Add other table cells as needed -->
                </tr>
            </tbody>
        </table>
    </div>
    <main>
    </main>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                loggedIn: false,
                usersList: null,
                stops: null,
                error_msg: null,
            }
        },
        created() {
            
        },
        methods: {
            setAllDisplaysNull() {
                this.usersList = null;
                this.stops = null;
                this.error_msg = null;
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
            getFormattedDate() {
                const today = new Date();
                const year = today.getFullYear();
                let month = today.getMonth() + 1;
                let day = today.getDate();

                // Add leading zero for single-digit months and days
                month = month < 10 ? `0${month}` : month;
                day = day < 10 ? `0${day}` : day;

                return `${year}-${month}-${day}`;
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
