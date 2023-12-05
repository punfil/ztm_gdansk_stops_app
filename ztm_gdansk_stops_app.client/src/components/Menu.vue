<template>
    <ul class="horizontal-menu">
        <li v-on:click="fetchUsers">List users</li>
        <li v-on:click="fetchStops">List stops</li>
        <li v-if="!this.$store.state.loggedIn" v-on:click="loginTrigger">Login</li>
        <li v-if="this.$store.state.loggedIn" v-on:click="listUserStops">List user favourite stops</li>
        <li v-if="this.$store.state.loggedIn" v-on:click="logout">Logout</li>
    </ul>
</template>

<script>
    export default {
        methods: {
            fetchUsers() {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                fetch('listusers')
                    .then(r => r.json())
                    .then(json => {
                        this.$store.commit('setUsersList', json);
                        this.$store.commit('setLoading', false);
                        return;
                    });
            },
            fetchStops() {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                fetch('stops')
                    .then(r => r.json())
                    .then(json => {
                        this.$store.commit('setStops', json[this.getFormattedDate()].stops);
                        this.$store.commit('setLoading', false);
                        return;
                    });
            },
            loginTrigger() {
                this.$store.commit('setAllDisplaysNull');
                this.$store.commit('setLoginMode', true);
            },
            listUserStops() {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                const url = `listuserstops/${this.$store.state.loggedIn.id}`;
                fetch(url)
                    .then(response => {
                        this.$store.commit('setLoading', false);
                        if (!response.ok) {
                            this.$store.commit('setMsg', "Failed to show user stops.");
                            return;
                        }

                        return response.json();
                    })
                    .then(json => {
                        this.$store.commit('setUserStops', json);
                        this.$store.commit('setLoading', false);
                        return;
                    });
            },
            logout() {
                this.$store.commit('setAllDisplaysNull');
                this.$store.commit('setLoggedIn', null);
            },
            getFormattedDate() {
                const today = new Date();
                const year = today.getFullYear();
                let month = today.getMonth() + 1;
                let day = today.getDate();

                month = month < 10 ? `0${month}` : month;
                day = day < 10 ? `0${day}` : day;

                return `${year}-${month}-${day}`;
            },
        }
    }
</script>

<style scoped>
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
</style>