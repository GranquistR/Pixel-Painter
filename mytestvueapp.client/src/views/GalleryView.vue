<script setup lang="ts">
    import { onMounted, ref } from "vue";
    import artworkData from "../dummydata/DummyArt"
    import ArtCard from "../components/ArtCard.vue"
    import Art from "@/entities/Art";
    import ArtAccessService from "@/utils/ArtAccessService";

    type WorksOfArt = {
        ArtId: number 
        ArtName: string 
        ArtistId: number
        Width: number
        ArtLength: number
        Encode: string
        CreationDate: string
        IsPublic: boolean
    }[];


    const stuff = ref<WorksOfArt | null>(null); 

    onMounted(() => {
        fetchData();
    });

    function fetchData() {
        stuff.value = null;

        fetch("artaccess/GetAllArt")
            .then((r) => r.json())
            .then((json) => {
                stuff.value = json as WorksOfArt;
                return;
            })
            .catch(console.error)
    }

    const artworks = ref(artworkData)

</script>


<template>
    <div class="w-8 mx-auto my-0">
        <div class="flex">
            <ArtCard v-for="art in artworks" :key="art.id" :art="art"/>
        </div>
        <div>
            {{ stuff }}
        </div>
    </div>
</template>