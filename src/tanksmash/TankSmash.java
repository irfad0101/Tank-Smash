
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
        gw.setVisible(true);
    }
    
}
