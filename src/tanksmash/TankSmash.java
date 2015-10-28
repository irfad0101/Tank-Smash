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
            /*GameObject go = new Tank(0,0);
            if (go.getClass()==Tank.class){
            System.out.println("Tank:getClass");
            }
            if (go instanceof Tank){
            System.out.println("Tank:instanceof");
            }
            */
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | UnsupportedLookAndFeelException ex) {
            System.out.println("Exception while setting Look and Feel");
        }
        GameWindow gw = new GameWindow();
        gw.setLocationRelativeTo(null);
        gw.setVisible(true);
        (new Thread(new NetworkHandler(7000, gw.getGameEngine()))).start();
    }
    
}
