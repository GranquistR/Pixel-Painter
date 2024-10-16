<template>
    <div class="w-auto">
        <div class="inline-block w-auto h-auto px-6 py-1 underline" id="User">{{userTest}}</div>
        <div class="inline-block w-auto h-auto" id="Comment">{{commentTest}}</div>
        <div class="inline-block w-auto h-auto px-3" id="Timestamp ">{{timeTest}}</div>
    </div>

</template>

<script setup lang="ts">
    import type Art from "@/entities/Art";
    import { onMounted, ref } from "vue";
    let commentTest = "This is a test comment and should look correct on the screen";
    let userTest = "TestUser";
    let timeTest = new Date();

    const props = defineProps<{
        art: Art;
        //need Query to grab comment based on ID
    }>();

   
</script>
