import Art from "../entities/Art";
import Comment from "../entities/Comment";

export default class ArtAccessService {
  public static async getAllArt(): Promise<any> {
    try {
      const response = await fetch("artaccess/GetAllArt");
      console.log("GetAll-Response: ", response);
      const json = await response.json();
      console.log("GetAll-JSONData: ", json);

      const allArt: Art[] = [];
      for (const art of json) {
        allArt.push(
          new Art(
            art.artId,
            art.artName,
            art.artistId,
            art.artistName,
            art.width,
            art.artLength,
            art.encode,
            art.creationDate,
            art.isPublic,
            art.numLikes,
            art.numComments
          )
        );
      }
      console.log("AllArt", allArt);

      return allArt;
    } catch (error) {
      console.error;
    }
  }

  public static async getArtById(artId: number): Promise<any> {
    try {
      const response = await fetch(`artaccess/GetArtById?id=${artId}`);
      console.log("ArtById-Response: ", response);
      const json = await response.json();
      console.log("ArtById-JSONData: ", json);

      const artpiece = json as Art;
      console.log("ArtById-jsonAsArt", artpiece);

      const newArtPiece = new Art(
        json.artId,
        json.artName,
        json.artistId,
        json.artistName,
        json.width,
        json.artLength,
        json.encode,
        json.creationDate,
        json.isPublic,
        json.numLikes,
        json.numcomments
      );
      console.log("ArtById-Pixelgrid", newArtPiece);

      return newArtPiece;
    } catch (error) {
      console.error;
    }
  }

  public static async getCommentsById(artId: number): Promise<any> {
    try {
      const response = await fetch(`artaccess/GetCommentsById?id=${artId}`);
      console.log("GetComments-Response: ", response);
      const json = await response.json();
      console.log("GetComments-JSONData: ", json);

      const allComments: Comment[] = [];
      for (const comment of json) {
        allComments.push(comment as Comment);
      }

      console.log(allComments);
      return allComments;
    } catch (error) {
      console.error;
    }
  }
}
