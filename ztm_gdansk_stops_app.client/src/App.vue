<script setup>

</script>

<template>
    <div :class="{ 'LightMode': this.$store.state.isLightMode, 'DarkMode': !this.$store.state.isLightMode }">
        <my-menu />
        <br />
        <author-header>
            <br />
            <header data-cy="msg" v-if="this.$store.state.msg" class="error">
                {{ this.$store.state.msg }}
            </header>
            <!---Placebo for loading -->
            <div v-if="this.$store.state.loading" class="loading">
                Fetching latest data just for you!
            </div>
            <!-- List users -->
            <user-list />
            <!-- List stops -->
            <stops-list />
            <!--- Stop info -->
            <stop-info />
            <!-- Add user form -->
            <add-user-form />
            <!-- Login form -->
            <login-form />
            <!-- List user stops -->
            <user-stops />
        </author-header>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
    })
</script>

<style>

.LightMode {
    background-color: white;
}
.DarkMode {
    background-color: black;
}

.highlight {
    color: red;
    font-weight: bold;
}
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
