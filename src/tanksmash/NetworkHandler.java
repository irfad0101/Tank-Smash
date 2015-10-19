/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package tanksmash;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author sachithra sahan
 */



public class NetworkHandler implements Runnable {
    
    private ServerSocket server;
    private int port;
    private GameWindow game;
    
    public NetworkHandler(int port, GameWindow game){
        this.port = port;
        this.game = game;
    }
    
    private void recieve(){
        while (true) {
            try {
                Socket socket = server.accept();
                BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                String reply = in.readLine();
                System.out.println(reply);
                socket.close();
                /*new Thread(){
                    
                }.start();*/
                game.decode(reply);//should implement decode method
            } catch (IOException ex) {
                Logger.getLogger(NetworkHandler.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    public static void send(String ipAddress, int port, String message) throws IOException{
            Socket socket = new Socket(ipAddress, port);
            DataOutputStream d = new DataOutputStream(socket.getOutputStream());
            d.writeBytes(message);
            socket.close();
            System.out.println("Request sent: "+message);
    }
    
    @Override
    public void run() {
        try {
            this.server = new ServerSocket(this.port);
            recieve();
        } catch (IOException ex) {
            Logger.getLogger(NetworkHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
}

