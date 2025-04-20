<template>
    <Button
        :label="connected ? 'Disconnect' : 'Connect'"
        :severity="connected ? 'danger': 'primary'"
        :disabled ="isGif"
        icon="pi pi-wifi"
        @click="ToggleModal()"
    />

    <Dialog v-model:visible="visible" modal :style="{width:'25rem'}" :show-header="false">
            <div class="inline-flex items-center justify-content-between w-full">
                <h1 class="m-0 align-self-center text-center">Connect to a group?</h1>
                <Button class="align-self-center mt-2" icon="pi pi-times" severity="secondary" text rounded @click="visible = false"/>
            </div>
        <!--
        <h4 style="margin:0; font-size: 12px;">This will disable: Adding/Removing Layers, Gravity functions</h4>
        -->
        <Tabs value="0">
            <TabList>
                <Tab value="0" @click="tab = 0">Join Group</Tab>
                <Tab value="1" @click="tab = 1">Create Group</Tab>
            </TabList>  
            <TabPanels class="pb-0 px-0 pt-1">
                <TabPanel value="0">
                    <div>
                        <!-- <h4 style="margin-top: 0px; margin-bottom: 0px"> Group Name | Active Member Count</h4>
                        <ScrollPanel style="width: 100; height: 100px; margin-bottom: 10px; margin-top: 0px;">
                        <div v-for="group in groups" :key="group.groupName" class="inline-flex items-center justify-center">
                            {{`Group Name: ${group.groupName} Member Count: ${group.memberCount}`}}
                        </div>
                        </ScrollPanel> -->

                        <DataTable v-if="groups.length > 0" :value="groups" scrollable scroll-height="200px">
                            <Column field="groupName" header="Name" class="w-6 p-1 h-1" ></Column>
                            <Column field="memberCount" header="Count" class="w-2 p-1 h-1"></Column>
                            <column class="w-1 p-1 h-1">
                                <template #body="{data}">
                                    <Button label="Join" class="p-1 m-0" @click="groupname = data.groupName; connect()"></Button>
                                </template>
                            </column>
                        </DataTable>
                        <span v-else class="mt-2">No Groups Are Online :(</span>
                    </div>
                </TabPanel>
                <TabPanel value="1">
                    <div class="flex pt-3 align-items-center gap-3">
                        <span>Group: </span>
                        <InputText
                        v-model="groupname"
                        placeholder="group-name"
                        class="w-full"
                        ></InputText>
                    </div>
                </TabPanel>
            </TabPanels>
        </Tabs>

        <template #footer v-if="tab==1">
            <Button
                label="Cancel"
                text
                severity="secondary"
                @click="visible = false"
                autofocus
            />
            <Button
                label="Create"
                severity="secondary"
                @click="connect()"
                autofocus
            />
        </template>
    </Dialog>
</template>

<script setup lang="ts">
    import { onMounted, ref, watch } from "vue";
    import Button from "primevue/button";
    import Dialog from "primevue/dialog";
    import InputText from "primevue/inputtext";
    import { ScrollPanel, DataTable, Column } from "primevue";
    import SocketService from "@/services/SocketService";
    import GroupAdvert from "@/entities/GroupAdvert";
    import {Tabs, TabList, Tab, TabPanels, TabPanel } from "primevue";

    const emit = defineEmits(["openModal","connect", "disconnect"]);

    const props = defineProps<{
        connected: boolean;
        isGif: boolean;
    }>();

    const visible = ref(false);
    const groupname = ref("");
    const groups = ref<GroupAdvert[]>([]);
    const tab = ref(0);

    function ToggleModal() {
        if (!props.connected) {
            visible.value = !visible.value;
        } else {
            disconnect();
        }
    }

    function connect() {
        emit("connect", groupname.value);
        visible.value = !visible.value;
    }

    function disconnect() {
        emit("disconnect");
        if (!props.connected) {
            ToggleModal();
        }
    }

    watch(visible, () => {
        emit("openModal", visible.value);
        SocketService.getAllGroups()
            .then((data) => {
                groups.value = data;
            });
    });
</script>
