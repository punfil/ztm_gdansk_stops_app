<template>
    <div v-if="this.$store.state.usersList" class="content">
        <button v-on:click="addUserTrigger">Add user</button>
        <h2>Users:</h2>
        <vue-good-table :columns="columns" :rows="this.$store.state.usersList">
            <template #table-row="props">
                <span v-if="props.column.field == 'action'">
                    <button v-on:click="deleteUser(props.row.Id)">Remove user</button>
                </span>
            </template>
        </vue-good-table>
    </div>
</template>

<script>
    import { ref } from 'vue';
    import { VueGoodTable } from 'vue-good-table-next';
    import 'vue-good-table-next/dist/vue-good-table-next.css'
    export default {
        components: {
            VueGoodTable,
        },
        setup() {
            const columns = ref([
                { label: 'Id', field: 'Id' },
                { label: 'Login', field: 'Login' },
                { label: 'Action', field: 'action', sortable: false },
            ]);

            return {
                columns,
            };
        },
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