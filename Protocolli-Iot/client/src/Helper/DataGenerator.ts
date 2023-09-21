import { faker } from "@faker-js/faker";
import { IPosition } from "src/IPosition";

export const generateData = (): IPosition => {
  let result = {} as IPosition;
  const location = faker.address.nearbyGPSCoordinate();

  result.latitude = Number(location[0]);
  result.longitude = Number(location[1]);
  result.DroneId = faker.helpers.arrayElement(["Drone1", "Drone2", "Drone3"]);
  result.time = new Date();
  result.speed = faker.datatype.number({ min: 0, max: 50 });
  result.altitude = faker.datatype.number({ min: 0, max: 200 });
  return result;
};