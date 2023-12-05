<template>
    <div v-if="this.$store.state.usersList" class="content">
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
                <tr v-for="user in this.$store.state.usersList" :key="user.Id">
                    <td>{{ user.Id }}</td>
                    <td>{{ user.Login }}</td>
                    <td v-if="user.Id != this.$store.state.loggedIn" v-on:click="deleteUser(user.Id)">Remove user</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    export default {
        methods: {
            addUserTrigger() {
                this.$store.commit('setAllDisplaysNull');
                this.$store.commit('setAddUserMode', true);
            },
            deleteUser(userID) {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                fetch('deleteuser/' + userID, { method: "DELETE" })
                    .then(response => {
                        this.$store.commit('setLoading', false);
                        if (!response.ok) {
                            this.$store.commit('setMsg', "Failed to remove user.");
                            return;
                        }

                        this.$store.commit('setMsg', "User removed!");
                        return;
                    })
            },
        }
    }
</script>