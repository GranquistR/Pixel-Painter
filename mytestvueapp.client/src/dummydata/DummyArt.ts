import type Art from "@/entities/Art";
import WorkOfArt from "../entities/WorkOfArt"

const WorksOfArt: Art[] = [
    { "artId": 3, "artName": "Self Portrait", "artistId": 3, "width": 20, "artLength": 20, "encode": "https://art.pixilart.com/d694f0570634497.png", "creationDate": "08/08/01", "isPublic": true },
    { "artId": 4, "artName": "Bold and Brash", "artistId": 4, "width": 20, "artLength": 20,"encode": "https://render.fineartamerica.com/images/rendered/default/print/6.5/8/break/images/artworkimages/medium/3/2-bold-and-brash-squidward.jpg","creationDate": "08/08/01", "isPublic": true},
    { "artId": 5, "artName": "David", "artistId": 5, "width": 20, "artLength": 20,"encode": "https://static.wikia.nocookie.net/spongebob/images/5/52/SB%27s_Statue.png", "creationDate": "08/08/01", "isPublic": true }
];

export default WorksOfArt;