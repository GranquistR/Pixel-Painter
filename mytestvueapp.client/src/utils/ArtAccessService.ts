import Art from "../entities/Art"
import GalleryArt from "../entities/GalleryArt"

export default class ArtAccessService {

    public static async getAllArt(): Promise<any> {
        try {
            const response = await fetch("artaccess/GetAllArt");
            console.log("GetAll-Response: ",response);
            const json = await response.json();
            console.log("GetAll-JSONData: ", json);

            return json as Art[];
        } catch (error) {
            console.error
        }
    }

    public static async getAllGalleryArt(): Promise<any> {
        try {
            const response = await fetch("artaccess/GetAllGalleryArt");
            console.log("GetAll-Response: ",response);
            const json = await response.json();
            console.log("GetAll-JSONData: ", json);

            return json as GalleryArt[];
        } catch (error) {
            console.error
        }
    }

    public static async getArtById(artId: number): Promise<any> {
        try {
            const response = await fetch(`artaccess/GetArtById?id=${artId}`);
            console.log("ArtById-Response: ",response);
            const json = await response.json();
            console.log("ArtById-JSONData: ", json);

            return json as Art;
        } catch (error) {
            console.error
        }
    }

}

