import Art from "../entities/Art";
import Comment from "../entities/Comment";

export default class ArtAccessService {
  public static async getAllArt(): Promise<Art[]> {
    try {
      const response = await fetch("/artaccess/GetAllArt");
      const json = await response.json();

      const allArt: Art[] = [];

      for (const jsonArt of json) {
        let art = new Art();
        art = jsonArt as Art;

        allArt.push(art);
      }

      return allArt;
    } catch (error) {
      console.error;
      throw error;
    }
  }

  public static async GetCurrentUsersArt(): Promise<Art[]> {
    try {
      const response = await fetch("/artaccess/GetCurrentUsersArt");

      if (!response.ok) {
        throw new Error("Error: Bad response");
      }

      const json = await response.json();
      const allArt: Art[] = [];

      for (const jsonArt of json) {
        let art = new Art();
        art = jsonArt as Art;

        allArt.push(art);
      }

      return allArt;
    } catch (error) {
      console.error;
      throw error;
    }
  }

  public static async getArtById(artId: number): Promise<Art> {
    try {
      const response = await fetch(`/artaccess/GetArtById?id=${artId}`);
      const json = await response.json();

      const artpiece = json as Art;

      return artpiece;
    } catch (error) {
      console.error;
      throw error;
    }
  }

  public static async getCommentsById(artId: number): Promise<Comment[]> {
    try {
      const response = await fetch(`/artaccess/GetCommentsById?id=${artId}`);
      const json = await response.json();

      const allComments: Comment[] = [];
      for (const comment of json) {
        allComments.push(comment as Comment);
      }

      return allComments;
    } catch (error) {
      console.error;
      throw error;
    }
  }

  public static async UploadArt(art: Art): Promise<Art> {
    try {
      art.creationDate = new Date().toISOString();

      const response = await fetch("/artaccess/SaveArt", {
        method: "POST",
        body: JSON.stringify(art),
        headers: { "Content-Type": "application/json" },
      });
      const json = await response.json();

      const artpiece = json as Art;

      return artpiece;
    } catch (error) {
      console.error(error);
      throw error;
    }
  }
}
