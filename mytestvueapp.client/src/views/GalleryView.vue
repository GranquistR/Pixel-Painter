<script setup>
    import { ref } from "vue";
    import artworkData from "../dummydata/DummyWorksOfArt"
    import ArtCard from "../components/ArtCard.vue"

    const artworks = ref(artworkData)


</script>


<template>
    <div class="container">
        <div class="art-container">
            <ArtCard v-for="art in artworks" :key="art.id" :art="art"/>
        </div>
    </div>
</template>

<style scoped>
    .container {
        width: 70%;
        margin: 0 auto;
    }

    .art-container {
        display: flex;
    }
</style>