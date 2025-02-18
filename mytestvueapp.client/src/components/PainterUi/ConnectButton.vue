<template>
    <Button
        :label="connected ? 'Disconnect' : 'Connect'"
        :severity="connected ? 'danger': 'primary'"
        icon="pi pi-wifi"
        @click="ToggleModal()"
    />

    <Dialog v-model:visible="visible" modal :style="{width:'25rem'}">
        <template #header>
            <h1 class="mr-2">Connect?</h1>
        </template>

        <div class="flex align-items-center gap-3">
            <span>Group: </span>
            <InputText
            v-model="groupname"
            placeholder="group-name"
            class="w-full"
            ></InputText>
        </div>

        <template #footer>
            <Button
                label="Cancel"
                text
                severity="secondary"
                @click="visible = false"
                autofocus
            />
            <Button
                label="Connect"
                severity="secondary"
                @click="connect()"
                autofocus
            />
        </template>
    </Dialog>
</template>

<script setup lang="ts">
    import { ref, watch } from "vue";
    import Button from "primevue/button";
    import Dialog from "primevue/dialog";
    import InputText from "primevue/inputtext";
    import * as SignalR from "@microsoft/signalr"

    const emit = defineEmits(["OpenModal","Connect"]);

    const visible = ref(false)
    const connected = ref(false);
    const groupname = ref("");

    let connection = new SignalR.HubConnectionBuilder()
            .withUrl("https://localhost:7154/signalhub", {
                skipNegotiation: true,
                transport: SignalR.HttpTransportType.WebSockets
            })
            .build();

    connection.on("ReceiveMessage", (user: string, msg: string) => {
        console.log("Received Message", user + " " + msg);
    });

    function ToggleModal() {
        if (!connected.value) {
            visible.value = !visible.value;
        } else {
            disconnect();
        }
    }

    function connect() {

        connection.start()
            .then(
                () => {
                    console.log("Connected to SignalR!");
                    connection.invoke("JoinGroup", groupname.value);
                    connected.value = !connected.value;
                    visible.value = !visible.value;
                }
            ).catch(err => console.error("Error connecting to Hub:",err));
    }

    function disconnect() {
        connection.invoke("LeaveGroup",groupname.value);
        //call after connection is terminated
        connected.value = !connected.value;
    }

    watch(visible, () => {
        emit("OpenModal", visible.value);
    });
</script>
