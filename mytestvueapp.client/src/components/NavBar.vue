<script setup>
    import { RouterLink } from 'vue-router';
</script>

<template>
    <div class="container">
        <h1 class="page-name">Page Name</h1>
        <div class="options">
            <RouterLink active-class="active" to="/">Home</RouterLink>
            <RouterLink active-class="active" to="/paint">Painter</RouterLink>
            <RouterLink active-class="active" to="/gallery">Gallery</RouterLink>
        </div>
        <div class="user-account-details">
            <h2>User Account Stuff</h2>
        </div>
    </div>
</template>

<style scoped>
    .container {
        display: flex;
        justify-content: space-between;
        align-items: center;

        padding: 5px;
    }

    .active {
        border: 2px solid blue;
        font-weight: bolder;
    }

    .options {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-wrap: nowrap;
        gap: 20px;
    }

    a {
        text-decoration: none;
        border-radius: 10%;
        border: 2px solid black;
        padding: 5px;
    }

</style>