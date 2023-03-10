{$CLEO}

const
    KEY = VK_F9 // The key that triggers the script
    DISTANCE_THRESHOLD = 5.0 // The minimum distance from the checkpoint to stop the vehicle

var
    waypoint_marker = -1 // The handle of the waypoint marker
    vehicle_handle = -1 // The handle of the vehicle the player is in

thread 'drive_to_waypoint'
    while true
        wait 0
        if is_key_pressed(KEY) and is_player_playing(PlayerChar)
            // Get the handle of the player's vehicle
            vehicle_handle = Car.GetPlayerCar(PlayerChar)

            // Get the position of the waypoint marker
            waypoint_marker = Map.GetFirstBlipInfoId(8)
            if waypoint_marker > 0
                waypoint_x = Blip.GetXCoord(waypoint_marker)
                waypoint_y = Blip.GetYCoord(waypoint_marker)

                // Drive the vehicle to the waypoint
                Car.SetSpeedInstantly(vehicle_handle, 30.0)
                Car.TurnTo(vehicle_handle, waypoint_x, waypoint_y)
                Car.DriveTo(vehicle_handle, waypoint_x, waypoint_y, 0.0)
                while Car.IsDriving(vehicle_handle)
                    wait 0
                    // Stop the vehicle when it reaches the checkpoint
                    distance = Car.DistanceToXYZ(vehicle_handle, waypoint_x, waypoint_y, 0.0)
                    if distance < DISTANCE_THRESHOLD
                        Car.SetSpeedInstantly(vehicle_handle, 0.0)
                        Car.Stop(vehicle_handle)
                        break
                    end
                end
            end
        end
    end
end_thread
