<script setup>
    import { RouterLink } from 'vue-router';
</script>

<template>
    <div class="container flex justify-content-between align-items-center p-2">
        <h1 class="page-name">Page Name</h1>
        <div class="options flex justify-content-center align-items-center flex-nowrap gap-3">
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
    .active {
        border: 2px solid blue;
        font-weight: bolder;
    }

    a {
        text-decoration: none;
        border-radius: 10%;
        border: 2px solid black;
        padding: 5px;
    }
</style>