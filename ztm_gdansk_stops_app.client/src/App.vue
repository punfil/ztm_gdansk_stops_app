<script setup>

</script>

<template>
        <my-menu />
        <br />
        <h1>
            RAILAB4 184657 Panfil Wojciech - zkm_gdansk_stops_app
        </h1>
        <br />
        <header v-if="this.$store.state.msg" class="error">
            {{ this.$store.state.msg }}
        </header>
        <!---Placebo for loading -->
        <div v-if="this.$store.state.loading" class="loading">
            Fetching latest data just for you!
        </div>
        <!-- List users -->
        <user-list/>
        <!-- List stops -->
        <stops-list/>
        <!--- Stop info -->
        <stop-info/>
        <!-- Add user form -->
        <div v-if="this.$store.state.addUserMode">
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
        <login-form/>
        <!-- List user stops -->
        <user-stops/>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            username: null;
            password: null
        },
        created() {
            
        },
        methods: {
            addUser() {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                const url = `adduser/${this.id}&${this.username}&${this.password}`;
                fetch(url, {
                    method: 'POST',
                })
                    .then(response => {
                        this.$store.commit('setLoading', false);
                        if (!response.ok) {
                            this.$store.commit('setMsg', "Failed to add user.");
                            return;
                        }

                        this.$store.commit('setMsg', "User added!");
                        return;
                    })
            },
        }
    })
</script>

<style>
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

h1 {
    font-family: 'Arial', sans-serif;
    color: #3498db; /* Blue color */
    text-align: center;
    padding: 20px;
    background-color: #ecf0f1; /* Light gray background color */
    border-bottom: 2px solid #bdc3c7; /* Gray border at the bottom */
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
