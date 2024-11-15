import type Artist from "@/entities/Artist";

export default class LoginService {
  public static async isLoggedIn(): Promise<boolean> {
    try {
      const response = await fetch("/login/IsLoggedIn");
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const isLoggedIn: boolean = (await response.json()) as boolean;
      return isLoggedIn;
    } catch (error) {
      console.error("Error checking login status:", error);
      return false;
    }
  }

  public static async logout(): Promise<void> {
    try {
      const response = await fetch("/login/Logout");
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
    } catch (error) {
      console.error("Error logging out:", error);
    }
  }

  public static async GetCurrentUser(): Promise<Artist> {
    try {
      const response = await fetch("login/GetCurrentUser");

      if (!response.ok) {
        console.log("Response was not ok");
        throw new Error("Error: Bad response");
      }

      const data = await response.json();
      const user: Artist = data;

      return user;
    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  public static async updateUsername(newUsername: any): Promise<boolean> {
    try {
      const response = await fetch(
        `login/UpdateUsername?newUsername=${newUsername}`
      );

      if (!response.ok) {
        throw new Error("Error: Bad response");
      }

      const data = await response.json();
      const success: boolean = data;

      return success;
    } catch (error) {
      console.error(error);
      return false;
    }
    }

    public static async getEmail(): Promise<string> {
        try {
            const response = await fetch("login/GetEmail");

            if (!response.ok) {
                throw new Error("Error: Bad response");
            }

            const email: string = await response.text();

            /*const contentType = response.headers.get("Content-Type");
            console.log('Content-Type:', contentType);*/

            return email;
        }
        catch (error) {
            console.error(error);
            return "";
        }
    }
}


