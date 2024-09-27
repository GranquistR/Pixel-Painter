<script setup>

const { art } = defineProps(['art'])
</script>

<template> 
    <div class="art-card">
        <img :src="art.image">
        <div class="card-text">
            <p>{{ art.title }}</p>
            <p>{{ art.artistName }}</p>
        </div>
    </div>
</template>

<style scoped>
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
    }

    .art-card {
        width: 300px;
        overflow: hidden;
        border-radius: 5%;
        margin-bottom: 20px;
        margin-right: 20px;
        cursor: pointer;

        background-color: #FFFFFF;
        box-shadow: 1px 1px 10px rgba(0, 0, 0, 0.2);
    }

    .art-card .card-text {
        display: flex;
        justify-content: space-between;
        flex-wrap: nowrap;
        padding: 0px 5px 0px 5px;
    }

    p {
        font-size: 12px;
    }

    .art-card img {
        height: 150px;
        width: 100%;
        margin: 0;
        padding: 0;
    }

</style>