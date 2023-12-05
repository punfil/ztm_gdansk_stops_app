<template>
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
</template>

<script>
    export default {
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
    }
</script>