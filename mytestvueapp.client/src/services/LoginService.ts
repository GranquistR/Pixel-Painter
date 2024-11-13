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
      public static async generateUsername(): Promise<string> {
        try {
            const response = await fetch("login/UsernameGenerator");
            
            if (!response.ok) {
                console.log("Response was not ok");
                throw new Error("Error: Bad response");
            }

            const data = await response.json();
            const username: string = data.username;

            return username;
        }
        catch (error) {
            console.error(error);
            return "Failed to generate username";
        }
    }

    public static async getUsername(): Promise<string> {
        try {
            const response = await fetch("login/GetUsername");

            if (!response.ok) {
                console.log("Response was not ok");
                throw new Error("Error: Bad response");
            }

            const data = await response.json();
            const username: string = data.username;

            return username;
        }
        catch (error) {
            console.error(error);
            return "Failed to get username";
        }
    }

    public static async updateUsername(newUsername : any): Promise<number> {
        try {
            const response = await fetch(`login/UpdateUsername?newUsername=${newUsername}`);

            if (!response.ok) {
                throw new Error("Error: Bad response");
            }

            const data = await response.json();
            const rowsChanged: number = data.rowsChanged;

            return rowsChanged;
        }
        catch (error) {
            console.error(error);
            return -1;
        }
    }
}
