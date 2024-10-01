import Art from "../entities/Art"


export default class ArtAccessService {

    public getAllArt(): Art[] {
       let worksOfArt: Art[] = [];

        fetch("artaccess/GetAllArt")
            .then((r) => r.json())
            .then((json) => {
                worksOfArt = json as Art[]
                return;
            })
            .catch(() => {
                console.log("Error Fetching Data");
            });

        return worksOfArt;
    }
}

