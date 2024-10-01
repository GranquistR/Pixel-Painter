import Art from "../entities/Art"


export default class ArtAccessService {

    public static getAllArt(): Art[] {
       const worksOfArt: Art[] = [];

       
        fetch("artaccess/GetAllArt", {
            headers: {
              "Content-Type": "application/json",
            }
            }).then((r) => r.json()) // Convert Response Stream to json
            .then((json) => { 
                console.log(json[1].artName)
                //worksOfArt = json as (Art[]);
                return;
            })
            .catch(console.error);

        return worksOfArt;
    }
}

