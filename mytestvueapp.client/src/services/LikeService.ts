export default class LikeService {
  public static async insertLike(artId: number): Promise<any> {
    try {
      const response = await fetch(`/like/InsertLike?artId=${artId}`);
      if (!response.ok) {
        throw new Error("Response was false.");
      }
      return true;
    } catch (error) {
      console.error(error);
      return false;
    }
  }
  public static async removeLike(artId: number): Promise<any> {
    try {
      const response = await fetch(`/like/RemoveLike?artId=${artId}`);
      if (!response.ok) {
        throw new Error("Response was false.");
      }
      return true;
    } catch (error) {
      console.error(error);
      return false;
    }
  }
  public static async isLiked(artId: number): Promise<any> {
    try {
      const response = await fetch(`/like/IsLiked?artId=${artId}`);
      if (!response.ok) {
        throw new Error("Response was false.");
      }
      const isLiked: boolean = (await response.json()) as boolean;
      return isLiked;
    } catch (error) {
      console.error(error);
      return false;
    }
  }
}
