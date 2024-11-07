export default class Comment {
    commentId?: number
    artistId?: string
    artistName?: string
    artId?: number
    commentContent?: string
    response?: number
    commentTime?: string

    constructor(
        commentId?: number,
        artistId?: string,
        artistName?: string,
        artId?: number,
        commentContent?: string,
        response?: number,
        commentTime?: string,
    ) {
        this.commentId = commentId
        this.artistId = artistId
        this.artistName = artistName
        this.artId = artId
        this.commentContent = commentContent
        this.response = response
        this.commentTime = commentTime
    }
} 