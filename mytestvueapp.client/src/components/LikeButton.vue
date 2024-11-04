<script setup lang="ts">
    import Button from "primevue/button";
    import { ref } from "vue";
    
    const props = defineProps<{ 
        likes: number;
    }>();

    const liked = ref(false);

    const likedClicked = () => {
      
    }
</script>

<template>
    <Button
        :severity="liked ? 'danger' : ''"
        class="w-full flex-grow p-1"
        :icon="liked ? 'pi pi-heart-fill' : 'pi pi-heart'"
        :label="likes"
        @click.stop="likedClicked()"
    />
</template>

<style scoped>
    
</style>
