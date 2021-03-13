export class AppSettings {
  public static API_ENDPOINT = 'https://localhost:44373/api';
  public static GET_CLIENT_TOKEN_URL = AppSettings.API_ENDPOINT + '/Payment/getclienttoken';
  public static CREATE_PURCHASE_URL = AppSettings.API_ENDPOINT + '/Payment/createpurchase';
}
