<script setup lang="ts">
    import { onMounted, ref } from "vue";
    import artworkData from "../dummydata/DummyArt"
    import ArtCard from "../components/ArtCard.vue"
    import Art from "@/entities/Art";
    import ArtAccessService from "@/utils/ArtAccessService";

    const artworks = ref(artworkData) // Dummy Data
    const stuff = ref<Art[] | null>(null); // Local call
    const stuff2 = ref<Art[] | null>(null); // Service call

    //ArtAccessService.getAllArt().then((r) => stuff2.value = r);

    onMounted(() => {
        fetchData();
        //ArtAccessService.getAllArt().then((promise) => stuff2.value = promise as Art[]);
    });

    function fetchData() {
        stuff.value = null;

        fetch("artaccess/GetAllArt")
            .then((r) => r.json())
            .then((json) => {
                stuff.value = json as Art[];
                return;
            })
            .catch(console.error)
    }
</script>


<template>
    <div class="w-9 mx-auto my-0">
        <div class="flex flex-wrap">
            <ArtCard v-for="art in artworks" :key="art.artId" :art="art"/> 
            <ArtCard v-for="art in stuff" :key="art.artId" :art="art" />
            <ArtCard v-for="art in stuff2" :key="art.artId" :art="art" />
        </div>
        <div>
            {{ stuff2 }}
        </div>
    </div>
</template>