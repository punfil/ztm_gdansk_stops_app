<script setup>
    import { ref } from 'vue'
    import DummyInput from './DummyInput.vue'
</script>

<template>
    <div v-if="this.$store.state.loginMode">
        <h2>Login:</h2>
        <form @submit.prevent="login">
            <label for="username">Username:</label>
            <DummyInput v-model="username" />
            <label for="password">Password:</label>
            <input type="password" v-model="password" required>
            <button type="submit">Login</button>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                username: '',
                password: '',
            };
        },
        methods: {
            login() {
                this.$store.commit('setLoading', true);
                this.$store.commit('setAllDisplaysNull');
                const url = `login/${this.username}&${this.password}`;
                fetch(url, {
                    method: 'POST',
                })
                    .then(response => {
                        this.$store.commit('setLoading', false);
                        if (!response.ok) {
                            this.$store.commit('setMsg', "Failed to login "+this.username);
                            return;
                        }

                        return response.json();
                    })
                    .then(json => {
                        this.$store.commit('setLoggedIn', json);
                        this.$store.commit('setLoading', false);
                    })
            }
        }
    }
</script>