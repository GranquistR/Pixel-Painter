export default class LoginService {
  public static async isLoggedIn(): Promise<boolean> {
    try {
      const response = await fetch("login/IsLoggedIn");
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
      const response = await fetch("login/Logout");
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
    } catch (error) {
      console.error("Error logging out:", error);
    }
  }

    public static async storeUserSub(): Promise<void> {
        try {
            const response = await fetch("login/StoreUserSub");
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
        } catch (error) {
            console.error(error);
        }
    }

    public static async generateUsername(): Promise<string> {
        try {
            const response = await fetch("login/UsernameGenerator");
            
            if (!response.ok) {
                console.log("Response was not ok");
                throw new Error("Error: Bad response");
            }

            const data = await response.json();
            const username: string = data.username;
           
            console.log("Username: ", username);

            return username;
        }
        catch (error) {
            console.error(error);
            return "Failed to generate username";
        }
    }
}
