import { Icon, Menu } from "semantic-ui-react";

export default function Navbar() {
    return (
      <Menu icon="labeled" fixed="top">
        <Menu.Item >
          <img src="assets\images\battleship-silhouette-8.png" alt="logo" />
        </Menu.Item>
        <Menu.Menu secondary position="right">
          <Menu.Item name="login" onClick={() => {}} icon="user outline" />
        </Menu.Menu>
      </Menu>
    );
}