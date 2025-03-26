export default class GIFCreationService {
    /*public static async createGIF(frames: string[]) { // : Promise<File>
        try {
            console.log("Frames: ", frames);

            const params = new URLSearchParams();
            frames.forEach(frame => params.append("frames", frame));

            const response = await fetch(`/gifcreation/CreateGIF?${params.toString()}`);
            //const response = await fetch(`/gifcreation/CreateGIF?frames=${frames}`);


            if (!response.ok) {
                console.error("Server error:", response.status, await response.text());
                throw new Error("Failed to create GIF");
            }

            const blob = await response.blob();

            console.log("Blob: ", blob);
            const url = window.URL.createObjectURL(blob);
            const link = document.createElement("a");
            link.href = url;
            link.download = "animation.gif";
            link.click();
        }
        catch (error) {
            console.error;
            throw error;
        }
    }*/

    public static async createGIF(frames: string[]) {
        try {
            const response = await fetch("/GIFCreation/CreateGif", {
                 method: "POST",
                 body: JSON.stringify(frames),
                 headers: { "Content-Type": "application/json" }
            });

            if (!response.ok) {
                console.error("Server error:", response.status, await response.text());
                throw new Error("Failed to create GIF");
            }

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const link = document.createElement("a");
            link.href = url;
            link.download = "animation.gif";
            link.click();
        } catch (error) {
            console.error("Error creating GIF:", error);
            throw error;
        }
    }
}