import Art from "../entities/Art"

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

