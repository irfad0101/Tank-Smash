/*
*   Tank Smash
*   M. A. I. Hussain 130209D
*   S. S. Samarasinghe 130525R
*/


package tanksmash;

import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;

public class TankSmash {

    public static void main(String[] args) {
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | UnsupportedLookAndFeelException ex) {
            System.out.println("Exception while setting Look and Feel");
        }
        GameWindow gw = new GameWindow();
        gw.setLocationRelativeTo(null);
        gw.setVisible(true);
        NetworkHandler nw = NetworkHandler.getInstance();
        nw.setPort(7000);
        nw.setGameEngine(gw.getGameEngine());
        (new Thread(nw)).start();
    }
    
}
