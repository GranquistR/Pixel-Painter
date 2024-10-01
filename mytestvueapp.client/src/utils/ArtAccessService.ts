import Art from "../entities/Art"


export default class ArtAccessService {

    public static async getAllArt(): Promise<Art[]> {
       const worksOfArt: Art[] = [];

       
        const response = fetch("artaccess/GetAllArt", {
            headers: {
              "Content-Type": "application/json",
            },
            })
            /*
            .then((r) => console.log(r)) // Convert Response Stream to json 
           /* .then((json) => { 
                console.log(json[0].artName)
                //worksOfArt = json as (Art[]);
                return;
            })
            .catch(console.error);*/

        console.log(response);
        console.log((await response).json());

        return worksOfArt;
    }
}

