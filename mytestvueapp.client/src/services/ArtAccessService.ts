import Art from "../entities/Art";
import Comment from "../entities/Comment";

export default class ArtAccessService {
  public static async getAllArt(): Promise<any> {
    try {
      const response = await fetch("/artaccess/GetAllArt");
      const json = await response.json();

      const allArt: Art[] = [];

      for (const jsonArt of json) {
        let art = new Art();
        art = jsonArt as Art;
        console.log("Art", art);
        allArt.push(art);
      }
      console.log("AllArt", allArt);

      return allArt;
    } catch (error) {
      console.error;
    }
  }

  public static async getArtById(artId: number): Promise<any> {
    try {
      const response = await fetch(`/artaccess/GetArtById?id=${artId}`);
      const json = await response.json();

      const artpiece = json as Art;

      return artpiece;
    } catch (error) {
      console.error;
    }
  }

  public static async getCommentsById(artId: number): Promise<any> {
    try {
      const response = await fetch(`/artaccess/GetCommentsById?id=${artId}`);
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
