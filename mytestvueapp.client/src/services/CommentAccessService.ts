import Comment from "../entities/Comment";

export default class CommentAccessService {

    public static async getCommentsById(artId: number): Promise<any> {
        try {
            const response = await fetch(`/comment/GetCommentsById?id=${artId}`);
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

    public static async isCookieCommentUser(artistId: number): Promise<any> {
        try {
            const response = await fetch(`/comment/CheckCookietoUser?id=${artistId}`);
            const isMyComment: boolean = (await response.json()) as boolean;
            return isMyComment;


        } catch (error) {
            console.error;
        }
    }
    public static async postComment(comment: string, ArtId: Number): Promise<any> {
        try {
            const response = await fetch(
                `/comment/postComment?comment=${comment}&ArtId=${ArtId}`,
            );
        } catch (error) {
            console.error;
        }
    }
}
