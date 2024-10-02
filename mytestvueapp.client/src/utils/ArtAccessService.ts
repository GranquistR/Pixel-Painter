import Art from "../entities/Art"


export default class ArtAccessService {

    public static getAllArt(): Art[] {
        console.log("Calling getAllArt()");
        let worksOfArt: Art[] = [];

        fetch("artaccess/GetAllArt")
            .then((r) => r.json())
            .then((json) => {
                console.log("returning json", json)
                worksOfArt = json as Art[];
                console.log("worksOfArt", worksOfArt)
                return;
            })
            .catch(() => {
                console.error;
            })

        console.log("Tail log", worksOfArt);
        return worksOfArt
    }

}

