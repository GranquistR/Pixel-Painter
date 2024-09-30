<script setup>
    import { RouterLink } from 'vue-router';
</script>

<template>
    <div class="flex justify-content-between align-items-center py-0">
        <h2 class="ml-3">Pixel Painter</h2>
        <div class="flex justify-content-center align-items-center flex-nowrap gap-3">
            <RouterLink active-class="active" to="/">Home</RouterLink>
            <RouterLink active-class="active" to="/paint">Painter</RouterLink>
            <RouterLink active-class="active" to="/gallery">Gallery</RouterLink>
        </div>
        <div class="user-account-details">
            <h2 class="mr-3">User Account</h2>
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